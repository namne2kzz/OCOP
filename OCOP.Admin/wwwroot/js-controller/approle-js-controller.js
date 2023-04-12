let approle = {
    init: function () {
        approle.registerEvent();
    },
    registerEvent: function () {
        $('.item-delete-click').each(function () {
            $(this).off('click').on('click', function (e) {
                e.preventDefault();
                var tr = $(this).closest('tr');
                Swal.fire({
                    title: 'Xóa Quyền có thể gây ra lỗi hệ thống',
                    text: "Bạn có chắc chắn muốn xóa Quyền này?",
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
                            url: "http://localhost:5000/AppRole/DeleteRole",
                            data: { roleId: $(this).data('id') },
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
    }
}
approle.init();