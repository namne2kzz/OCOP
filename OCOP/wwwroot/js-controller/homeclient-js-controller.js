let hoso = {
    init: function () {
        hoso.registerEvent();
    },
    registerEvent: function () {
        $("#sllNganh").off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                hoso.loadNhom(id);
            }
            else {
                $('#sllNhom').html('');
            }
        });
        $("#sllNhom").off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                hoso.loadPhanNhom(id);
            }
            else {
                $('#sllPhanNhom').html('');
            }
        });
    },
    loadNhom: function (id) {
        $.ajax({
            url: 'http://localhost:5001/HomeClient/LoadNhomByNganh',
            data: { nganhId: id },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="" selected="true" disabled="disabled">---Chọn Nhóm---</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.nhomId + '">' + item.tenNhom + '</option>'
                    })
                    $('#sllNhom').html(html);
                }
            }
        })
    },
    loadPhanNhom: function (id) {
        $.ajax({
            url: 'http://localhost:5001/HomeClient/LoadPhanNhomByNhom',
            data: { nhomId: id },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="" selected="true" disabled="disabled">---Chọn Phân Nhóm---</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.phanNhomId + '">' + item.tenPhanNhom + '</option>'
                    })
                    $('#sllPhanNhom').html(html);
                }
            }
        })
    }
};
hoso.init();

