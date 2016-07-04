//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('service', ['$scope', '$http', '$controller', '$timeout', 'serviceService',
  function ($scope, $http, $controller, $timeout, serviceService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = serviceService;

      $scope.formViewTemplate = '/Angular/Views/Admin/Service/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };

      $scope.loadDataForm = function (data) {
          serviceService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.openModal('/Angular/Views/Admin/Service/edit.html');

          });
      };

      //$scope.loadPropertyFilter();
      $scope.loadData();

  }]);

'use strict';
web365app.controller('serviceModified', ['$scope', '$http', '$controller', 'serviceService',
  function ($scope, $http, $controller, serviceService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          for (var instanceName in CKEDITOR.instances) {
              CKEDITOR.instances[instanceName].updateElement();
          }

          $scope.dataForm = $(form.target).serialize();

          serviceService.modified($scope.dataForm).then(function (res) {

              $scope.resetList();

              $scope.cancel();
          });
      };

  }]);