//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('detailService', ['$scope', '$http', '$controller', '$timeout', 'detailService',
  function ($scope, $http, $controller, $timeout, detailService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = detailService;

      $scope.formViewTemplate = '/Angular/Views/Admin/DetailService/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };

      $scope.loadDataForm = function (data) {
          detailService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listService;

              $scope.openModal('/Angular/Views/Admin/DetailService/edit.html');

          });
      };

      //$scope.loadPropertyFilter();
      $scope.loadData();

  }]);

'use strict';
web365app.controller('detailServiceModified', ['$scope', '$http', '$controller', 'detailService',
  function ($scope, $http, $controller, detailService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          for (var instanceName in CKEDITOR.instances) {
              CKEDITOR.instances[instanceName].updateElement();
          }

          $scope.dataForm = $(form.target).serialize();

          detailService.modified($scope.dataForm).then(function (res) {

              $scope.resetList();

              $scope.cancel();
          });
      };

  }]);