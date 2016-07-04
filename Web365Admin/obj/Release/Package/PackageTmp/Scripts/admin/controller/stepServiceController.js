//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('stepService', ['$scope', '$http', '$controller', '$timeout', 'stepService',
  function ($scope, $http, $controller, $timeout, stepService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = stepService;

      $scope.formViewTemplate = '/Angular/Views/Admin/StepService/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };

      $scope.loadDataForm = function (data) {
          stepService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listService;

              $scope.openModal('/Angular/Views/Admin/StepService/edit.html');

          });
      };

      //$scope.loadPropertyFilter();
      $scope.loadData();

  }]);

'use strict';
web365app.controller('stepServiceModified', ['$scope', '$http', '$controller', 'stepService',
  function ($scope, $http, $controller, step) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          for (var instanceName in CKEDITOR.instances) {
              CKEDITOR.instances[instanceName].updateElement();
          }

          $scope.dataForm = $(form.target).serialize();

          step.modified($scope.dataForm).then(function (res) {

              $scope.resetList();

              $scope.cancel();
          });
      };

  }]);