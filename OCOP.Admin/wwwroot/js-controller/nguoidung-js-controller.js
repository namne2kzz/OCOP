let nguoidung = {
    init: function () {
        nguoidung.registerEvent();
    },
    registerEvent: function () {
        $('.item-trangthai-click').each(function () {
            $(this).off('click').on('click', function (e) {
                e.preventDefault();
                var link = $(this);
                $.ajax({
                    url: "http://localhost:5000/AppUser/ChangeTrangThai",
                    data: { appUserId: $(this).data('id') },
                    dataType: "json",
                    type: "POST",
                    success: function (res) {
                        if (res.status == true) {
                            if (res.data == true) {
                                link.text('Hoạt động');
                            }
                            else {
                                link.text('Khóa');
                            }
                        }
                    }
                });
            });
        });

        /*Review Image Before Upload*/
        $('#file').on('change', function () {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image').src = e.target.result;
            };

            reader.readAsDataURL(this.files[0]);
        });      

        $('.item-delete-click').each(function () {
            $(this).off('click').on('click', function (e) {
                e.preventDefault();
                var tr = $(this).closest('tr');
                Swal.fire({
                    title: 'Xóa người dùng',
                    text: "Bạn có chắc chắn muốn xóa người dùng này?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes',
                    cancelButtonText: 'Cancle',
                    // customClass: 'swal-wide'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: "http://localhost:5000/NguoiDung/DeleteNguoiDung",
                            data: { appUserId: $(this).data('id') },
                            dataType: "json",
                            type: "POST",
                            success: function (res) {
                                if (res.status == true) {
                                    tr.remove();
                                    Swal.fire(
                                        'Thành công',
                                        res.message,
                                        'success'
                                    );
                                }
                            }
                        });
                    }
                });
            });
        });
        $("#sllHuyen").off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                nguoidung.loadXa(id)
            }
            else {
                $('#sllXa').html('');
            }
        });
    },
    loadXa: function (id) {
        $.ajax({
            url: 'http://localhost:5000/NguoiDung/LoadXaByHuyen',
            data: { huyenId: id },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="" selected="true" disabled="disabled">---Chọn Xã---</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.xaId + '">' + item.tenXa + '</option>'
                    })
                    $('#sllXa').html(html);
                }
            }
        })
    }
}
nguoidung.init();