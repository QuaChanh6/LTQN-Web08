let format = {};

/**
 * Hàm kiểm tra rỗng/null/undefined
 * LTQN(9/9/2022)
 * @param {*} data 
 * @returns true/false
 */
format.checkEmptyData = (data) =>{
    if(data === '' || data === undefined || data === null){
        return true;
    }       
    else{
        data = data.toString().trim();
        if(data === '') return true;
        return false; 
    }
}
/**
 * Hàm forrmat date
 * @param {date} dateSrc
 * author: LTQN(9/9/2022)
 */
format.formatDate = (dateSrc) => {
    if (dateSrc != null && dateSrc.length > 10) {
        let dateString = dateSrc.slice(0, 10);
        let date = new Date(dateString);
        let year = date.getFullYear().toString(), month = (date.getMonth() + 1).toString().padStart(2, "0"), day = date.getDate().toString().padStart(2, "0");
        return `${day}/${month}/${year}`;
    }
    return "";
}  

 /**
   * Hàm xử lý dữ liệu tiếng việt
   * author: LTQN(11/9/2022)
   * @param {String} xử lý dữ liệu trong tìm kiếm
   */
format.processData = (Text) => {
  try {
    Text = Text.trim().toLowerCase();
      //Xoa khoảng trắng
      Text = Text.replace(/\s\s+/g,' ');
      //Xóa dấu tiếng việt trong chuỗi
      Text = Text.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
      Text = Text.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
      Text = Text.replace(/ì|í|ị|ỉ|ĩ/g, "i");
      Text = Text.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
      Text = Text.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
      Text = Text.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
      Text = Text.replace(/đ/g, "d");
      Text = Text.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, "");
      
      return Text;
  } catch (error) {
    console.log(error)
  }
      
  }
/**
 * Hàm kiểm tra định dạng mail
 * author: LTQN(19/9/2022)
 * @param {*} email 
 * @returns true/false
 */
format.Email = (email) => {
      if(!format.checkEmptyData(email)){ 
        // eslint-disable-next-line  
        let regexEmail= /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if(regexEmail.test(email)){
            return true;
        }
        return false
      }
    return true;
}

/**
 * Hàm so sánh ngày tháng
 * @param {string} date1, date2
 * @returns 
 */
format.checkDate = (date1, date2) => {
  date1 = new Date(date1);
  date2 = new Date(date2);
    if(!format.checkEmptyData(date1) && !format.checkEmptyData(date2)){
      if(date1.getTime() > date2.getTime())
        return false;
    }
    return true;
}

format.checkDate2 = (date1, date2) => {
  date1 = new Date(date1);
  date2 = new Date(date2);
    if(!format.checkEmptyData(date1) && !format.checkEmptyData(date2)){
      if(date1.getTime() >= date2.getTime())
        return false;
    }
    return true;
}

format.distanceDate = (date1, date2) => {
  date1 = new Date(date1);
  date2 = new Date(date2);
  let ms1 = date1.getTime();
  let ms2 = date2.getTime();
  return Math.ceil((ms2 - ms1) / (24*60*60*1000)) +1;
}


format.formatMoney = money =>{
  if(money && !isNaN(money)){

      return money.toString().replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");
  }else{
      return money;
  }
};


export default format;
