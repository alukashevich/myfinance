import { Component, OnInit } from '@angular/core';
import { Register, Login } from './../../models';
import { DataService } from './../../core/services';

@Component({
  selector: 'app-register',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  loginModel: Login = new Login();

  constructor(protected dataService: DataService) { }

  ngOnInit() {
    this.loginModel = new Login();
  }

  register() {
    this.dataService.create(Register, this.loginModel).subscribe((response: any) => {
      console.log(response.Data.userName + " " + response.Data.password);
    }, (error: any) => {
        console.log(error);
    });
  }
}
