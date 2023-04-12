let appuser = {
    init: function () {
        appuser.registerEvent();
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
                    title: 'Xóa thành viên',
                    text: "Bạn có chắc chắn muốn xóa thành viên này?",
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
                            url: "http://localhost:5000/AppUser/DeleteThanhVien",
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
    },   
}
appuser.init();

   
