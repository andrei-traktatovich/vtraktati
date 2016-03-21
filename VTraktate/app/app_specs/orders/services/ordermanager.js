/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/angular.min.js" />
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/angular-mocks.js" />
/// <reference path="../../../infrastructure/momentjs/moment.js" />

/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/underscore.min.js" />
/// <reference path="C:\Users\nikolaev\Documents\Visual Studio 2013\Projects\1011\VTraktate\VTraktate\Scripts/toastr.min.js" />
/// <reference path="../../../infrastructure/localStorage/localStorage.svc.js" />
/// <reference path="../../../infrastructure/dateTimeUtils/dateTimeUtils.mdl.js" />
/// <reference path="../../../infrastructure/dateTimeUtils/taskDurationManager.js" />


/// <reference path="../../../thirdPartyServices/thirdPartyServices.js" />
/// <reference path="../../../globals/globals.js" />
/// <reference path="../../../orders/utilities/volumeRoundingRules/volumeRoundingRules.js" />
/// <reference path="../../../orders/utilities/utilities.mdl.js" />
/// <reference path="../utilities/jobInfoStorage.js" />

/// <reference path="../../../orders/services/ordermanager.svc.js" />


describe('orderManager', function () {
    var sut;
      
    beforeEach(function () {
        module('orders.utilities');
        inject(function (orderManager) { sut = orderManager; });
        inject(function (taskDurationManager) {
            spyOn(taskDurationManager, 'defaultJobEndDate').and.returnValue('some end date');
        });
    });

    describe('customerId', function () {
        var jobs, order;
        beforeEach(function () {
            jobs = [{}, {}];
            order = { jobs: jobs };
        }); 

        it('setCustomerId propagates customerId through all jobs', function () {
            var result = sut.setCustomerId(order, 1);
            expect(result.jobs[0].customerId).toEqual(1);
            expect(result.jobs[1].customerId).toEqual(1);
        });
    });

    describe('initialTotal', function () {
        it('if jobs array is undefined returns 0', function () {
            var result = sut.initialTotal();
            expect(result).toEqual(0);
        })

        it('if jobs array is empty return 0', function () {
            var result = sut.initialTotal([]);
            expect(result).toEqual(0);
        });

        it('ignores job items without pricing and/or volume', function () {
            var jobs = [{}, {}];
            var result = sut.initialTotal(jobs);
            expect(result).toEqual(0);
        })

        it('calculates initial price', function () {
            var jobs = [
                {
                    initial: {
                        pricing: {
                            discountedPrice: 5
                        }
                    }
                },
                {
                    initial: {
                        pricing: {
                            discountedPrice: 5
                        }
                    }
                }
            ];
            
            var result = sut.initialTotal(jobs);
            expect(result).toEqual(10);
        });

    });

    describe('create', function () {
        var fakeOfficeID = 1;
        it('should throw if not officeId provided', function () {
            expect(function () { sut.create(); }).toThrow();
        });

        describe('without template should create empty order', function () {
            var order;
            beforeEach(function () { order = sut.create({}, fakeOfficeID, 'some start date', 'some end date'); });

            it('and set its jobs to empty array', function () {
                var isArray = angular.isArray(order.jobs);
                expect(isArray).toEqual(true);

                expect(order.jobs.length).toEqual(0);
            });

            it('and set its officeId to office id', function () {
                expect(order.officeId).toEqual(fakeOfficeID);
            })

            it('and set its start and end date', function () {
                expect(order.startDate).toEqual('some start date');
                expect(order.plannedDeliveryDate).toEqual('some end date');
            });

            it('and set default start & end date', function () {
                order = sut.create({}, fakeOfficeID);
                expect(order.startDate).toBeDefined();
                expect(order.plannedDeliveryDate).toEqual('some end date');
            });
        });

        describe('with template', function () {
            var order, template = {
                jobs: [1, 2, 3],
                officeId: 666,
                someProp: 'someProp'  
            };
            beforeEach(function () {
                order = sut.create(template, fakeOfficeID);
            });

            it('should extend empty order with template', function () {
                expect(order.jobs).toEqual([1, 2, 3]);
                expect(order.someProp).toEqual('someProp');
            });
             
            it('should override default order with template properties', function () {
                expect(order.officeId).toEqual(666);
            })
        });

    });
});