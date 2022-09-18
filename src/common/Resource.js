  let Resource = {};


Resource.ToastMessage = {
    success: 'Thành công',
    error: 'Lỗi'
}
Resource.dataDropList = [ //các chức ăng
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

Resource. textNumPages = [
  {
    num: '10',
    textData: '10 bản ghi trên một trang'
  },
  {
    num: '20',
    textData: '20 bản ghi trên một trang'
  },
  {
    num: '30',
    textData: '30 bản ghi trên một trang'
  },
  {
    num: '50',
    textData: '50 bản ghi trên một trang'
  },
  {
    num: '100',
    textData: '100 bản ghi trên một trang'
  },
],

Resource.toolDropList = {
  Duplacate: 'Duplicate',
  Delete: 'Delete',
  Stop: 'Stop'
}

Resource.popupWarning = {
  EmptyName: {
    name: 'EmptyName',
    content: "Tên không được để trống"
  },
  EmptyCode: {
    name: 'EmptyCode',
    content: "Mã nhân viên không được để trống"
  },
  EmptyDepartment: {
    name: 'EmptyDepartment',
    content: "Đơn vị không được để trống"
  }
}


Resource.Url = 'https://cukcuk.manhnv.net/api/v1/';
export default Resource;