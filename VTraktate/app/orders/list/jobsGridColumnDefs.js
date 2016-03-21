(function () {
    angular.module('order.list') 
    .service('jobsGridColumnDefs', jobsGridColumnDefs);

    function jobsGridColumnDefs(constants) {

        // TODO: map icon for order group payment status 

        //function mapJobPaymentStatus(grid, row, col, rowRenderIndex, colRenderIndex) {
        //    var classes = {
        //        0: 'glyphicon glyphicon-remove-sign text-danger',
        //        1: 'job-invoiced',
        //        2: 'glyphicon glyphicon-alert text-alert',
        //        3: 'glyphicon glyphicon-ok-sign text-success'
        //    };

        //    if (row.entity.$$treeLevel == 0 && row.entity.type === constants.ROW_TYPES.JOB_GRID_ROW_TYPE_JOB && !row.entity.parentParticipant)
        //        return classes[row.entity.paymentStatus.id];
        //    else return '';
        //}

        var columnDefs = [
              /*{
                  name: 'paymentStatus',
                  field: 'paymentStatus',
                  displayName: ' ',
                  headerTooltip: 'Статус оплаты',
                  cellClass: mapJobPaymentStatus,
                  cellTemplate: 'job-payment-cell.tpl.html'
              },*/
              {
                  displayName: 'Дата сдачи',
                  name: 'endDate',
                  field: 'endDate',
                  headerTooltip: 'Дата сдачи',
                  cellFilter: 'dateTimeFilter'
              },

              {
                  displayName: 'Исполнитель',
                  name: 'providerName',
                  field: 'provider.name',
                  cellTemplate: 'provider-cell-template.tpl.html',
                  headerTooltip: 'Исполнитель'
              },

              {
                  name: 'customer',
                  field: 'customer.shortName',
                  displayName: 'Заказчик',
                  headerTooltip: 'Заказчик',
                  cellTemplate: 'customer-cell-template.tpl.html'
              },
              {
                  name: 'orderName',
                  field: 'order.name',
                  displayName: 'заказ №',
                  cellTemplate: 'order-name-cell-template.tpl.html'
              },
              {
                  name: 'jobTypeName',
                  field: 'jobType.name',
                  displayName: 'Вид работ',
                  headerTooltip: 'Вид работ'
              },
              {
                  name: 'languageName',
                  field: 'language.name',
                  displayName: 'Язык',
                  headerTooltip: 'Направление перевода'
              },
              {
                  name: 'document',
                  field: 'document',
                  displayName: 'Документ',
                  headerTooltip: 'Документ',
                  cellTemplate: 'document-cell-template.tpl.html'
              },

              // первоначальный объем заказа
              {
                  name: 'initialVolumeChars',
                  field: 'initial.volume.chars',
                  displayName: 'Исход.знаков',
                  headerTooltip: 'Исходных знаков'
              },
              {
                  name: 'initialVolumePages',
                  field: 'initial.volume.pages',
                  displayName: 'Исход.стр.',
                  headerTooltip: 'Исход.стр.'
              },
              {
                  name: 'initialVolumeWords',
                  field: 'initial.volume.words',
                  displayName: 'Исход.слов',
                  headerTooltip: 'Исход.слов'
              },

              // первоначальная стоимость
              {
                  name: 'initialRate',
                  field: 'initial.pricing.rate',
                  displayName: 'Исход.цена/ед.',
                  headerTooltip: 'Предварительная цена за единицу'
              },
              {
                  name: 'initialPrice',
                  field: 'initial.pricing',
                  displayName: 'Исход.стоимость',
                  headerTooltip: 'Предварительная стомисть (c учетом скидки)',
                  cellTemplate: 'price-info-cell-template.tpl.html'
              },

              // конечные данные о заказе/исполнителе
              // окончательный объем заказа
              {
                  name: 'finalVolumeChars',
                  field: 'final.volume.chars',
                  displayName: 'Знаков',
                  headerTooltip: 'Знаков'
              },
              {
                  name: 'finalVolumePages',
                  field: 'final.volume.pages',
                  displayName: 'Уч.стр.',
                  headerTooltip: 'Учетных страниц'
              },
              {
                  name: 'finalVolumeWords',
                  field: 'final.volume.words',
                  displayName: 'Слов',
                  headerTooltip: 'Слов'
              },

              // первоначальная стоимость
              {
                  name: 'finalRate',
                  field: 'final.pricing.rate',
                  displayName: 'Ццена/ед.',
                  headerTooltip: 'Цена за единицу'
              },
              {
                  name: 'finalPrice',
                  field: 'final.pricing',
                  displayName: 'Стоимость',
                  headerTooltip: 'Стоимость (c учетом скидки)',
                  cellTemplate: 'price-info-cell-template.tpl.html'
              },
        ];

        return columnDefs;
    }

})();
