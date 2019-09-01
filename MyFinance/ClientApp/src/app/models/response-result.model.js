"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ResponseResult = /** @class */ (function () {
    function ResponseResult(IsSuccess, Message, Data) {
        if (IsSuccess === void 0) { IsSuccess = true; }
        if (Message === void 0) { Message = 'Done'; }
        if (Data === void 0) { Data = null; }
        this.IsSuccess = IsSuccess;
        this.Message = Message;
        this.Data = Data;
    }
    return ResponseResult;
}());
exports.ResponseResult = ResponseResult;
//# sourceMappingURL=response-result.model.js.map