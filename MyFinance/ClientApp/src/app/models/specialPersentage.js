"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var calc_model_1 = require("./calc.model");
var SpecialPersentage = /** @class */ (function () {
    function SpecialPersentage(id, monthCount, monthPercentage, credit) {
        if (id === void 0) { id = '00000000-0000-0000-0000-000000000000'; }
        if (monthCount === void 0) { monthCount = 12; }
        if (monthPercentage === void 0) { monthPercentage = 12.5; }
        if (credit === void 0) { credit = new calc_model_1.Calc(); }
        this.id = id;
        this.monthCount = monthCount;
        this.monthPercentage = monthPercentage;
        this.credit = credit;
    }
    return SpecialPersentage;
}());
exports.SpecialPersentage = SpecialPersentage;
//# sourceMappingURL=specialPersentage.js.map