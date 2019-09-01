import { Login } from './login.model'
export class Register extends Login{
  constructor(
    public confirmPassword: string = ''
  ) {
      super();
  }
}
