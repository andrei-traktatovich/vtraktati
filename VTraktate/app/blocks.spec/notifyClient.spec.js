/// <reference path="../../app.test-helpers/app-test-helpers.js"/>
/// <reference path="../helpers/create-module.js"/>
/// <reference path="../blocks/notifyClient.svc.js"/>

describe("notifyClient", () => {
    var notifyClient, toastr;

    function now() {
        return "some_fake_time";
    }

    beforeEach(module("blocks.notifyClient"));
    beforeEach(module({
        toastr: {
            error: () => {},
            success: () => {},
            info: () => {},
            warning: () => {}
        },
        now: () => "some_time"
    }));

    beforeEach(inject((_notifyClient_, _toastr_) => {
        notifyClient = _notifyClient_;
        toastr = _toastr_;
        spyOn(toastr, "error");
        spyOn(toastr, "warning");
        spyOn(toastr, "info");
        spyOn(toastr, "success");
    }));

    it("sends an error message", () => {
        notifyClient.error("some error", "some text", "some error object");
        expect(toastr.error).toHaveBeenCalledWith("some error", "some text");
    });

    it("when sending error message, stores it in local storage", () => {
        spyOn(JSON, "stringify").and.callThrough();
        notifyClient.error("some error", "some text", "some error object");
        expect(localStorage.getItem(`ERR${"some error"} : ${now()}`)).toBeDefined();
        expect(JSON.stringify).toHaveBeenCalled();
    });

    it("sends an error message", () => {
        notifyClient.error("some error", "some text", "some error object");
        expect(toastr.error).toHaveBeenCalledWith("some error", "some text");
    });

    it("sends an info message", () => {
        notifyClient.info("some error", "some text", "some error object");
        expect(toastr.info).toHaveBeenCalledWith("some error", "some text");
    });

    it("sends a success message", () => {
        notifyClient.success("some error", "some text", "some error object");
        expect(toastr.success).toHaveBeenCalledWith("some error", "some text");
    });

    it("sends a success message", () => {
        notifyClient.warning("some error", "some text", "some error object");
        expect(toastr.warning).toHaveBeenCalledWith("some error", "some text");
    });

})