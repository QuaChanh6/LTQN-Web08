let format = {};

format.checkEmptyData = (data) =>{
    if(data === '' || data == undefined || data == null){
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
    if (dateSrc != null) {
        let dateString = dateSrc.slice(0, 10);
        let date = new Date(dateString);
        let year = date.getFullYear().toString(), month = (date.getMonth() + 1).toString().padStart(2, "0"), day = date.getDate().toString().padStart(2, "0");
        return `${day}/${month}/${year}`;
    }
    return "";
}  


export default format;
