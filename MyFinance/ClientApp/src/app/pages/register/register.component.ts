import { Component, OnInit } from '@angular/core';
import { Register } from './../../models';
import { DataService } from './../../core/services';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
  registerModel: Register = new Register();

  constructor(protected dataService: DataService) { }

  ngOnInit() {
    this.registerModel = new Register();
  }

  register() {
    this.dataService.create(Register, this.registerModel).subscribe((response: any) => {
      console.log(response.Data.userName + " " + response.Data.password);
    }, (error: any) => {
        console.log(error);
    });
  }
}
