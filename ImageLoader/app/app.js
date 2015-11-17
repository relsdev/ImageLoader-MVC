var app = angular.module('app', []);

app.controller('homeCtrl', function ($http) {

    var vm = this;
    vm.images = [];
    vm.newUrl = 'http://a5.mzstatic.com/us/r30/Purple7/v4/ab/af/3e/abaf3e37-3582-80d0-c489-5fd91ae3b145/icon256.png';

    vm.addImage = function () {
        $http.put('api/image', { Url: vm.newUrl }).success(function (data) {
            vm.images.push({ url: data.Url, site: getDomain(data.Url), id: data.Id });
        });
    }

    vm.removeImage = function (image) {
        $http.delete('api/image', { params: { id: image.id, blobId: (image.url.split('/').pop()).trim() } }).success(function (data) {
            var index = vm.images.indexOf(image);
            vm.images.splice(index, 1);
        });
    }

    $http.get('api/image').success(function (data) {
        for (var i = 0; i < data.length; i++) {
            vm.images.push({ url: data[i].Url, site: data[i].Domain, id: data[i].Id });
        }
    });

    getDomain = function (url) {
        var a = document.createElement('a');
        a.href = url;

        var domain = a.hostname;;
        a.remove();

        return domain;
    }
});