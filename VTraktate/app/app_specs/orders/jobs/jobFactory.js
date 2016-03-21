/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/angular.min.js" />
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/angular-mocks.js" />

/// <reference path="../../../infrastructure/constants.js" />
/// <reference path="../../../orders/jobs/jobFactory.js" />

describe('jobFactory', function () {
    var job, sut, defaultCurrency, defaultStatusId, defaultUOM;

    beforeEach(function () {
        module('order.job');
        inject(function (jobFactory, constants) {
            sut = jobFactory;
            defaultCurrency = constants.CURRENCIES.RUBLE;
            defaultStatusId = constants.JOB_STATUSES.JOB_STATUS_CREATED;
            defaultUOM = constants.DEFAULTJOBUOM;
        });
        
    })

    describe('create', function () {
        beforeEach(function () {
            job = sut.create();
        });

        it('creates empty job with default currency', inject(function (jobFactory) {
            expect(job.currencyId).toEqual(defaultCurrency);
        }));

        it('creates empty job with default statusId', inject(function (jobFactory) {
            expect(job.statusId).toEqual(defaultStatusId);
        }));

        it('creates empty job with default UOM', inject(function (jobFactory) {
            expect(job.UOMId).toEqual(defaultUOM);
        }));
    });

    describe('create from template', function () {
        beforeEach(function () {
            job = sut.create({
                testProperty: 'testProperty',
                statusId: 'testStatusId'
            });
        })

        it('template properties override prototype properties', function () {
            expect(job.statusId).toEqual('testStatusId');
        })

        it('template properties extend prototype properties', function () {
            expect(job.testProperty).toEqual('testProperty');
        })
    });

    describe('append', function () {
        var jobs;
        beforeEach(function () {
            jobs = null;
        });

        it('creates a default job if jobs array undefined', function () {
            var job = sut.add();
            var job1 = sut.create();
            expect(job).toEqual(job1);
        });

        it('creates a default job if jobs array empty', function () {
            var job = sut.add([]);
            var job1 = sut.create();
            expect(job).toEqual(job1);
        })

        it('copies last item from array if array not empty', function () {
            var lastitem = { prop: 1 };
            var jobs = [{ prop: 0 }, lastitem ];
            var job = sut.add(jobs);
            expect(job).toEqual(lastitem);
        });
    });
});