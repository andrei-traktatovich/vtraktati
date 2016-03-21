/// <reference path="../../../app.test-helpers/app-test-helpers.js"/>

/// <reference path="../filters/addHoursToLocalTime.filter.js"/>


describe("addHoursToLocalTimeFilter", () => {

    var filter;
    function moment() {
        var that = this;
        this.add = (number, string) => {
            this.number = number;
            this.string = string;
            return that;
        }

        this.format = () => that;
        return this;
    }

    beforeEach(module("traktat.filters"));

    beforeEach(() => {
        angular.module("traktat.filters")
            .factory("moment", () => moment);
    });

    beforeEach(inject(($filter) => {
        filter = $filter("addHoursToLocalTime");
        expect(filter).toBeDefined();
    }));

    it("adds the hour number to the current time", () => {
        var result = filter(7);
        expect(result.number).toEqual(7);
        expect(result.string).toEqual("h");
    });

     
})