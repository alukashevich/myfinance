import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class AccountService {
  public username: string = '';
  public authKey: string = '';

  constructor(protected router: Router) { }

  isUserLoged(): boolean {
    return this.username.trim() != '';
  }

  public login(name: string) {
    this.username = name;
    this.router.navigate(
      ['/dashboard']
    );
  }

  public logout() {
    this.username = '';
    this.authKey = '';
    this.router.navigate(
      ['/account/login']
    );
  }
}
