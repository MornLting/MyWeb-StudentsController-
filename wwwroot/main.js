function DeleteWithController(id, controller) {
    Swal.fire({
        title: "�Ƿ�ȷ��ɾ��?",
        text: "�˲������ɳ���!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "��",
        cancelButtonText: "��",
        animation: false
    }).then(result => {
        if (result.isConfirmed) {
            fetch('/' + controller + '/Delete/' + id)
                .then(response => {
                    if (response.ok) {
                        return response.text().then(text => {
                            Swal.fire({
                                title: text,
                                icon: "success"
                            }).then(() => location.reload());
                        });
                    }
                    else {
                        return response.text().then(text => {
                            Swal.fire({
                                title: "ɾ��ʧ��",
                                text: text,
                                icon: "error"
                            });
                        });
                    }
                });
        }
    });
}