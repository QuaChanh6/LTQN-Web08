<template>
    <div class="b-form">
        <div class="container"></div>
        <div class="b-form-container">
            <div class="b-form-header">
                <h2>THÊM MỚI ĐƠN TỪ - {{title}}</h2>
                <div class="icon-x" @click="closeForm"></div>
            </div>
            <div class="b-form-body">
                <div class="container-b-input">
                    <label>Họ và tên</label>
                    <input disabled type="text" class="input"  v-model="emp.employeeName">
                </div>
                <div class="container-b-input">
                    <label>Mã nhân viên</label>
                    <input disabled type="text" class="input" v-model="emp.employeeCode">
                </div>
                <div class="container-b-input" v-if="typeForm != 2">
                    <label>Ngày bắt đầu</label>
                    <input type="date" class="input"  v-model="ap.dateStart">
                </div>
                <div class="container-b-input" v-if="typeForm != 2">
                    <label>Ngày kết thúc</label>
                    <input type="date" class="input" v-model="ap.dateEnd">
                </div>
                <div class="container-b-input" v-if="typeForm == 2">
                    <label>Số tiền muốn ứng</label>
                    <input type="text" class="input" placeholder="Nhập số tiền"  v-model="money">
                </div>
                <div class="container-b-input">
                    <label>Lý do</label>
                    <textarea class="input"  v-model="ap.reason"></textarea>
                </div>
                <div class="container-b-input">
                    <label>Người quản lý</label>
                    <input disabled type="text" class="input" v-model="manager.employeeName">
                </div>
            </div>
            <div class="form-footer" style="justify-content: end;">
            <div class="cancel" style="margin-right: 16px">
                <MButton class="btn-cancel" :text="'Hủy'"/>
            </div>
            <div class="save">
                <MButton class="btn-save-and-add" :text="'Lưu'" @click="saveForm"/>
            </div>
        </div>
        </div>
        
    </div>
  </template>
  
  <script>

  import MButton from './MButton.vue';

   /**
     * hàm xử lý kết quả trả về
     * author: LTQN(10/9/2022)
     * @param {object} res: kết quả trả về của API
     * @param {int} pointer: {this, mode}
     */
     async function handleResponse(res, pointer){
        try {
              // thực hiện thành công
            if(res.ok){
                pointer.closeForm();
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                
            }
    
        } catch (error) {
            console.log(error);
        }

    }
  export default {
    components: { MButton},
    props:{
        typeForm: Number,
    },
    created(){
        this.code = sessionStorage.getItem("code");
        let url = process.env.VUE_APP_URL + "Employees/code/" + this.code;
            fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.emp = res[0];              
  
                     url = this.Url + "AppForms/Manager/" + this.emp.departmentID;
                        fetch(url)
                            .then(res => res.json())
                            .then(res => {
                                this.manager = res[0];              
                    
                            }).catch(error => {
                            console.log(error);
                        }) 

                }).catch(error => {
                  console.log(error);
            }) 
            // xử lý dữ liệu
            switch(this.typeForm){
                case 0:
                    this.title = 'XIN NGHỈ PHÉP';
                    break;
                case 1:
                    this.title = 'LÀM THÊM GIỜ';
                    break;
                case 2:
                this.title = 'ỨNG LƯƠNG';
                break;
            }
            this.ap.type = this.typeForm;
    },
    methods:{
        closeForm(){
            this.$emit("closeForm");
        },
        async saveForm(){
            this.ap.status = 0;
            this.ap.employeeName = this.emp.employeeName;
            this.ap.employeeCode = this.emp.employeeCode;
            this.ap.manager = this.manager.employeeName;
            let response = await fetch(`${this.Url}AppForms`,{
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            method: "POST",
            body: JSON.stringify(this.ap)
            })

            handleResponse(response, this);
        }
    },
    data(){
        return{
            ap: {},
            code: '',
            emp:{},
            manager: {},
            Url: process.env.VUE_APP_URL,
            title: '',
            money: ''
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../css/base/form.css);
  </style>
  