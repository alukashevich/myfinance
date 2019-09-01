import { SpecialPersentage } from "./specialPersentage";
import { MonthPayment } from "./monthPayment";

export class Calc {
  [key: string]: any;
  constructor(
    public id: string = '00000000-0000-0000-0000-000000000000',
    public amount: number = 50000,
    public basePercenteges: number = 14,
    public sumPercenteges: number = 0,
    public monthsCount: number = 180,
    public creditType: number = 1,
    public specialPersentages: Array<SpecialPersentage> = [],
    public monthPayments: Array<MonthPayment> = []
  ) { }
}
