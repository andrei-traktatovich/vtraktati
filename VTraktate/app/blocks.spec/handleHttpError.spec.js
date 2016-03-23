/// <reference path="../../app.test-helpers/app-test-helpers.js"/>
/// <reference path="../helpers/create-module.js"/>
/// <reference path="../blocks/blocks.module.js"/>
/// <reference path="../blocks/handleHttpError.svc.js"/>

describe("handleHttpError", () => {
    "use strict";
    var handleHttpError, $rootScope;

    beforeEach(module("blocks.error-handling"));

    beforeEach(inject((_handleHttpError_, _$rootScope_) => {
        handleHttpError = _handleHttpError_;
        $rootScope = _$rootScope_;
    }));

    it("is a function", () => {
        expect(typeof handleHttpError).toEqual("function");
    });

    it("return a function", () => {
        expect(typeof handleHttpError("some message")).toEqual("function");
    });

    it("fires an http-error event on rootScope", () => {
        var spy = jasmine.createSpy(),
            err = { status: 404, data: { message: "shit has happened " } },
            errorMessage = "some error";

        $rootScope.$on("http-error", spy);
        handleHttpError(errorMessage)(err);
        expect(spy).toHaveBeenCalled();
        expect(spy.calls.first().args[1]).toEqual({
            title: errorMessage,
            text: `${err.status} ${err.data.message}`,
            error: err
        });
    });

    it("message, statuscode and error from http error & stack are fed http-error event", () => {
        var spy = jasmine.createSpy(),
            err = { status: 404, data: { message: "shit has happened " }, stack: "some stack" },
            errorMessage = "some error";

        $rootScope.$on("http-error", spy);
        handleHttpError(errorMessage)(err);
        expect(spy).toHaveBeenCalled();
        expect(spy.calls.first().args[1]).toEqual({
            title: errorMessage,
            text: `${err.status} ${err.data.message}`,
            error: err
        });
    });
});
