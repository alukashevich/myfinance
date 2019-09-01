import { Calc } from "./calc.model";

export class MonthPayment {
  constructor(
    public id: string = '00000000-0000-0000-0000-000000000000',
    public number: number = 0,
    public payment: number = 0,
    public persentegesPyment: number = 0,
    public persentege: number = 0,
    public debitPayment: number = 0,
    public debit: number = 0,
    public credit: Calc = new Calc(),
  ) { }
}
