"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var calc_model_1 = require("./calc.model");
var MonthPayment = /** @class */ (function () {
    function MonthPayment(id, number, payment, persentegesPyment, persentege, debitPayment, debit, credit) {
        if (id === void 0) { id = '00000000-0000-0000-0000-000000000000'; }
        if (number === void 0) { number = 0; }
        if (payment === void 0) { payment = 0; }
        if (persentegesPyment === void 0) { persentegesPyment = 0; }
        if (persentege === void 0) { persentege = 0; }
        if (debitPayment === void 0) { debitPayment = 0; }
        if (debit === void 0) { debit = 0; }
        if (credit === void 0) { credit = new calc_model_1.Calc(); }
        this.id = id;
        this.number = number;
        this.payment = payment;
        this.persentegesPyment = persentegesPyment;
        this.persentege = persentege;
        this.debitPayment = debitPayment;
        this.debit = debit;
        this.credit = credit;
    }
    return MonthPayment;
}());
exports.MonthPayment = MonthPayment;
//# sourceMappingURL=monthPayment.js.map