/// <reference path="../../../app.test-helpers/app-test-helpers.js"/>

/// <reference path="../filters/addPlus.filter.js"/>


describe("addPlusFilter", () => {

    var filter;

    beforeEach(module("traktat.filters"));

    beforeEach(inject(($filter) => {
        filter = $filter("addPlus");
        expect(filter).toBeDefined();
    }));

    it("returns negative number as string", () => {
        expect(filter(-1)).toEqual("-1");
    });

    it("returns zero as zero string", () => {
        expect(filter(0)).toEqual("0");
    });

    it("returns positive with plus", () => {
        expect(filter(1)).toEqual("+1");
    });
})