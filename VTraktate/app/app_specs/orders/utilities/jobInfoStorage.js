
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/angular.min.js" />
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/angular-mocks.js" />
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/underscore.min.js" />
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/toastr.min.js" />
/// <reference path="../../../infrastructure/localStorage/localStorage.svc.js" />

/// <reference path="../../../thirdPartyServices/thirdPartyServices.js" />
/// <reference path="../../../globals/globals.js" />
/// <reference path="../../../orders/utilities/volumeRoundingRules/volumeRoundingRules.js" />
/// <reference path="../../../infrastructure/dateTimeUtils/dateTimeUtils.mdl.js" />

/// <reference path="../../../orders/utilities/utilities.mdl.js" />
/// <reference path="../../../orders/utilities/jobInfoStorage.js" />


describe('jobInfoStorage', function () {
    var sut, localStorageService;

    beforeEach(module('orders.utilities'));
    
    beforeEach(inject(function (LocalStorageService) {

        var store = {};

        spyOn(LocalStorageService, 'get').and.callFake(function (key) {
            return store[key];
        });
        spyOn(LocalStorageService, 'set').and.callFake(function (key, value) {
            return store[key] = value + '';
        });

        localStorageService = LocalStorageService;
    }));

    beforeEach(inject(function (jobInfoStorage) {
        sut = jobInfoStorage;
    }));

    it('make key "customerID jobTypeId languageId"', function () {
        var key = '429 34 35';
        var result = sut.makeKey({
            customerId: 429,
            jobTypeId: 34,
            languageId: 35
        });
        expect(result).toEqual(key);
    });

    it('saves job rate and currency in local storage', function () {
        var job = {
            customerId: 1,
            jobTypeId: 1,
            languageId: 1,
            currencyId: 1,
            initial: { pricing: { rate: 1 } }
        };

        sut.store(job);
        expect(localStorageService.set).toHaveBeenCalledWith('1 1 1', '{"rate":1,"currencyId":1}');
    });

    it('calls local storage to get job rate and currency', function () {
        var job = {
            customerId: 1,
            jobTypeId: 1,
            languageId: 1,
            currencyId: 1,
            initial: { pricing: { rate: 1 } }
        };

        sut.store(job);
        job.currencyId = null;
        job.initial.pricing.rate = null;
        sut.suggestRate(job);
        expect(localStorageService.get).toHaveBeenCalledWith('1 1 1');
        
    });

    it('sets job rate from local storage', function () {
        var job = {
            customerId: 1,
            jobTypeId: 1,
            languageId: 1,
            currencyId: 1,
            initial: { pricing: { rate: 1 } }
        };

        sut.store(job);
        job.currencyId = null;
        job.initial.pricing.rate = null;
        var result = sut.suggestRate(job);
        
        expect(result.initial.pricing.rate).toEqual(1);
    });

    it('sets job currency from local storage', function () {
        var job = {
            customerId: 1,
            jobTypeId: 1,
            languageId: 1,
            currencyId: 1,
            initial: { pricing: { rate: 1 } }
        };

        sut.store(job);
        job.currencyId = null;
        job.initial.pricing.rate = null;
        sut.suggestRate(job);
        
        expect(job.currencyId).toEqual(1);
        
    });

});