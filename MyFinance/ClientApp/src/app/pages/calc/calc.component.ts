import { Component } from '@angular/core';
import { DataService } from './../../core/services/data.service';
import { Calc } from '../../models/calc.model';
import { SpecialPersentage } from '../../models/specialPersentage';
import { BrowserModule } from '@angular/platform-browser'
import { FormBuilder, FormGroup, Validators, FormArray, FormControl, Form } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MonthPayment } from '../../models/monthPayment';

@Component({
  selector: 'app-calc',
  templateUrl: './calc.component.html'
})
export class CreditComponent {
  name: string;
  calcForm: FormGroup;
  calcData: Calc = new Calc();

  newPayment: number;
  newPymentDuration: number;

  paymentVisible: boolean;
  ngOnInit() {
    this.paymentVisible = false;
    this.createCalcForm();
    let creditId = localStorage.getItem('creditId');
    if (creditId !== null) {
      this.dataService.get(Calc, creditId)
        .subscribe((response: any) => {
          if (!response.IsSuccess) {
            this.createNewCredit();
          } else {
            this.calcData = response.Data;
            this.calcData.monthPayments = this.calcData.monthPayments.sort((n1, n2) => n1.number - n2.number)
            this.setCalcForm();
          }
        }, (error: any) => {
          console.error(error);
        });
    }
    else {
      this.createNewCredit();
    }
  }

  recalcPayments(value) {
    let updatedPayments = new Array<MonthPayment>();
    let paymentNumber = 0;
    for (let j = 0; j < value.paymentSets.length; j++) {
      var paymentSet = value.paymentSets[j];
      if (paymentSet.newPymentDuration == 0 || paymentSet.newPayment === 0)
        continue;
      for (let i = paymentNumber; i < paymentSet.newPymentDuration + paymentNumber; i++) {
        let payment = this.calcData.monthPayments[i];
        payment.payment = paymentSet.newPayment;
        updatedPayments.push(payment);
      }
      paymentNumber += paymentSet.newPymentDuration;
    }
    this.dataService.post(MonthPayment, "calculatelist/" + this.calcData.id, updatedPayments)
      .subscribe((response: any) => {
        if (!response.IsSuccess) {
          console.error(response.Message);
        } else {
          this.calcData = response.Data;
          if (this.calcData.monthPayments.length > 0)
            this.paymentVisible = true;
        }
      }, (error: any) => {
        console.error(error);
      });
  }

  submit(value) {
    value.id = this.calcData.id;
    this.dataService.post(Calc, "calculate", value)
      .subscribe((response: any) => {
        if (!response.IsSuccess) {
          console.error(response.Message);
        } else {
          if (value.paymentSets !== null && value.paymentSets.length > 0)
            this.recalcPayments(value);
          else {
            this.calcData = response.Data;
            if (this.calcData.monthPayments.length > 0)
              this.paymentVisible = true;
          }
        }
      }, (error: any) => {
        console.error(error);
      });
  }

  constructor(private fb: FormBuilder, protected dataService: DataService) { }

  addGracePeriod() {
    const control = <FormArray>this.calcForm.controls['specialPersentages'];
    let specialPeriod = new SpecialPersentage();
    specialPeriod.credit = this.calcData;
    this.dataService.create(SpecialPersentage, specialPeriod)
      .subscribe((response: any) => {
        if (!response.IsSuccess) {
          console.error(response.Message);
        } else {
          control.push(this.fb.group(response.Data));
        }
      }, (error: any) => {
        console.error(error);
      });
  }

  addOverridedPayments() {
    const control = <FormArray>this.calcForm.controls['paymentSets'];
    let paymentSet = {
      newPayment: 0,
      newPymentDuration: 0
    };
    control.push(this.fb.group(paymentSet));
  }

  removeGracePeriod(num: number) {
    const control = <FormArray>this.calcForm.controls['specialPersentages'];
    let id = (<FormGroup>control.controls[num]).controls["id"].value;
    this.dataService.delete(SpecialPersentage, id)
      .subscribe((response: any) => {
        if (!response.IsSuccess) {
          console.error(response.Message);
        } else {
          control.removeAt(num);
        }
      }, (error: any) => {
        console.error(error);
      });
  }

  setCalcForm() {
    this.calcForm.controls['amount'].setValue(this.calcData.amount);
    this.calcForm.controls['basePercenteges'].setValue(this.calcData.basePercenteges);
    this.calcForm.controls['monthsCount'].setValue(this.calcData.monthsCount);
    const specialPersentagesControl = <FormArray>this.calcForm.controls['specialPersentages'];
    for (let i = 0; i < this.calcData.specialPersentages.length; i++) {
      specialPersentagesControl.push(this.fb.group(this.calcData.specialPersentages[i]));
    }
    if (this.calcData.monthPayments.length > 0)
      this.paymentVisible = true;
  };

  createCalcForm() {
    this.calcForm = this.fb.group({
      amount: 0,
      basePercenteges: 0,
      monthsCount: 0,      
      specialPersentages: this.fb.array([]),
      paymentSets: this.fb.array([
        //this.fb.group({
        //  newPayment: 0,
        //  newPymentDuration: 0,
        //})
      ])
    });
  }

  createNewCredit() {
    this.calcData = new Calc(null, 50000, 14, 180, null);
    this.dataService.create(Calc, this.calcData)
      .subscribe((response: any) => {
        if (!response.IsSuccess) {
          console.error(response.Message);
        } else {
          localStorage.setItem('creditId', response.Data.id);
          this.calcData = response.Data;
          this.createCalcForm();
        }
      }, (error: any) => {
        console.error(error);
      });
  }
}
