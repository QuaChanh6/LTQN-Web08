var Enumeration = {};

// Giới tính
Enumeration.gender = {
    Female: 0, // Nữ
    Male: 1,   // Nam
    Other: 2   // Khác
}


Enumeration.SaveForm = {
    Save: 1, //lưu
    SaveAndAdd: 2 //lưu và thêm
}

Enumeration.Mode = {
    ADD: 1, //thêm
    EDIT: 2 //sửa
}
Enumeration.keyCode = { //sk bàn phím
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,
    ENTER: 13
  }

export default Enumeration;