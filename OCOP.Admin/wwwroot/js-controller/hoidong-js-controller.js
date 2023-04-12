let hoidong = {
    init: function () {
        hoidong.registerEvent();
    },
    registerEvent: function () {
        $('.item-trangthai-click').each(function () {
            $(this).off('click').on('click', function (e) {
                e.preventDefault();
                let div = $(this).closest('div');
                let td = $(this).closest('td');
                Swal.fire({
                    title: 'Bắt đầu đánh giá hồ sơ',
                    text: "Bạn có chắc chắn muốn đánh giá hồ sơ? Hội đồng sẽ không thể chỉnh sửa bất kì thông tin nào khác!",
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
                            url: "http://localhost:5000/HoiDong/LockHoiDong",
                            data: { hoiDongId: $(this).data('id') },
                            dataType: "json",
                            type: "POST",
                            success: function (res) {
                                if (res.status == true) {
                                    location.reload();
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
    }
}
hoidong.init();