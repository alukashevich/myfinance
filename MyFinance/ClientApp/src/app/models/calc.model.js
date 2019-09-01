"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Calc = /** @class */ (function () {
    function Calc(id, amount, basePercenteges, sumPercenteges, monthsCount, creditType, specialPersentages, monthPayments) {
        if (id === void 0) { id = '00000000-0000-0000-0000-000000000000'; }
        if (amount === void 0) { amount = 50000; }
        if (basePercenteges === void 0) { basePercenteges = 14; }
        if (sumPercenteges === void 0) { sumPercenteges = 0; }
        if (monthsCount === void 0) { monthsCount = 180; }
        if (creditType === void 0) { creditType = 1; }
        if (specialPersentages === void 0) { specialPersentages = []; }
        if (monthPayments === void 0) { monthPayments = []; }
        this.id = id;
        this.amount = amount;
        this.basePercenteges = basePercenteges;
        this.sumPercenteges = sumPercenteges;
        this.monthsCount = monthsCount;
        this.creditType = creditType;
        this.specialPersentages = specialPersentages;
        this.monthPayments = monthPayments;
    }
    return Calc;
}());
exports.Calc = Calc;
//# sourceMappingURL=calc.model.js.map