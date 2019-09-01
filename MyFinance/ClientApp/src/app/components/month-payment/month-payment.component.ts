import { Input, Component } from '@angular/core';
import { DataService } from './../../core/services/data.service';
import { Calc } from '../../models/calc.model';
import { SpecialPersentage } from '../../models/specialPersentage';
import { MonthPayment } from '../../models/monthPayment';

@Component({
  selector: 'month-payment',
  templateUrl: './month-payment.component.html'
})

export class MonthPaymentComponent {

  @Input() monthPayment: MonthPayment;

  constructor(protected dataService: DataService) {
  }
}
