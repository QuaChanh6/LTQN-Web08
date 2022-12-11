var enumeration = {};

// Giới tính
enumeration.gender = {
    Male: 0, // Nam
    Female: 1,   // Nữ
    Other: 2   // Khác
}
//tình trạng cong việc
enumeration.status = {
    Work: 0, // Nam
    Pause: 1,   // Nữ
    TryingOut: 2   // Khác
}

enumeration.SaveForm = {
    Save: 1, //lưu
    SaveAndAdd: 2, //lưu và thêm
}

enumeration.Mode = {
    ADD: 1, //thêm
    EDIT: 2 //sửa
}

enumeration.keyCode = { //sk bàn phím
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,
    ENTER: 13,
    TAB: 9,
    ESC: 27
}

enumeration.Status = {
    Stop: 0,
    Use: 1
}


export default enumeration;