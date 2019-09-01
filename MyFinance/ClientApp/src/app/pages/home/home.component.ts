import { Component, OnInit } from '@angular/core';
import { Login } from './../../models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  login: Login = new Login();
  constructor() { }
  ngOnInit() {
    this.login = new Login();
  }
}
