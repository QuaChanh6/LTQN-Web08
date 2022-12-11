  let resource = {};

/**
 * Nội dung toast
 */
resource.ToastMessage = {
    success: 'Thành công',
    error: 'Lỗi',
    info: 'Chức năng chưa được thi công'
}
/**
 * Loại toast
 */
resource.ToastMessageType = {
  success: 'toast-success',
  error: 'toast-error',
  info: 'toast-info'
}

resource.dataDropList = [ //các chức năng
    {
      type: 'Duplicate',
      text: 'Nhân bản'
    },
    {
      type: 'Delete',
      text: 'Xóa'
    },
    {
      type: 'Stop',
      text: 'Ngừng sử dụng'
      }
    ,

]

/**
 * droplist bản ghi
 */
resource. textNumPages = [
  {
    num: 10,
    textData: '10 bản ghi trên một trang'
  },
  {
    num: 20,
    textData: '20 bản ghi trên một trang'
  },
  {
    num: 30,
    textData: '30 bản ghi trên một trang'
  },
  {
    num: 50,
    textData: '50 bản ghi trên một trang'
  },
  {
    num: 100,
    textData: '100 bản ghi trên một trang'
  },
],

/**
 * các chức năng
 */
resource.toolDropList = {
  Duplicate: 'Duplicate',
  Delete: 'Delete',
  Stop: 'Stop',
}

/**
 * Các kiểu popup
 */
resource.popupWarning = {
  EmptyName: {
    name: 'EmptyName',
    content: "Tên không được để trống."
  },
  EmptyCode: {
    name: 'EmptyCode',
    content: "Mã nhân viên không được để trống."
  },
  EmptyDepartment: {
    name: 'EmptyDepartment',
    content: "Đơn vị không được để trống."
  }
}

/**
 * Sự kiện
 */
resource.event = {
  Click: 'click'
}

/**
 * Các kiểu popup
 */
resource.PopUpType = {
  Warning : 'icon-warning',
  Info: 'icon-info',
  Question: 'icon-question'
}


resource.overView = {
  employee: 'overView-employee',
  department: 'overView-department',
  position: 'overView-position'
}

export default resource;