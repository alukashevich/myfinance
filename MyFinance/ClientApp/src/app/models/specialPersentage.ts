import { Calc } from "./calc.model";

export class SpecialPersentage {
  [key: string]: any;
  constructor(
    public id: string = '00000000-0000-0000-0000-000000000000',
    public monthCount: number = 12,
    public monthPercentage: number = 12.5,
    public credit: Calc = new Calc(),
  ) { }
}
