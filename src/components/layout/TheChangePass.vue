<template>
    <div class="change-pass">
        <div class="container"></div>
        <div class="form-change">
            <div class="change-header">
                <h2>SỬA MẬT KHẨU</h2>
                <div class="icon-x" @click="close"></div>
                
            </div>
            <div class="b-form-body">
                    <div class="container-b-input">
                        <label>Mật khẩu hiện tại:</label>
                        <input type="password" class="input"  v-model="old">
                        </div>
                    <div class="container-b-input">
                        <label>Mật khẩu mới:</label>
                        <input type="password" class="input"  v-model="newpass">
                    </div>
                    <div class="container-b-input">
                        <label>Nhập lại mật khẩu mới:</label>
                        <div class="valid">
                            <input type="password" class="input"  v-model="newpassagain">
                            <div v-if="valid" style="color:red;">Mật khẩu không trùng khớp!</div>
                            <div v-if="error" style="color:red;">Sai mật khẩu hiện tại!</div>
                        </div>
                    </div>
                </div>
                <div class="form-footer" style="justify-content: end;">
            <div class="cancel" style="margin-right: 16px">
                <MButton class="btn-cancel" :text="'Hủy'" @click="close"/>
            </div>
            <div class="save">
                <MButton class="btn-save-and-add" :text="'Lưu'" @click="saveChange"/>
            </div>
        </div>
        </div>

        
    </div>
</template>
  
<script>
  import MButton from '../base/MButton.vue';

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
                pointer.$emit("closeChange");
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                pointer.error = true;
            }
    
        } catch (error) {
            console.log(error);
        }
    }

export default {
    components: { MButton},
    methods:{
        close(){
            this.$emit("closeChange");
        },
        async saveChange(){
            this.valid = false;
            this.error = false;
            if(this.newpassagain != this.newpass){
                this.valid = true;
            }else{

                let user={
                    employeeCode: sessionStorage.getItem("code"),
                    PassWord: this.newpass,
                    NameOfUser:  sessionStorage.getItem("user"),
                    oldPass: this.old
                }

                let response = await fetch(`${this.Url}Users/PassWord/${sessionStorage.getItem("code")}`,{
                    headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                    method: "POST",
                    body: JSON.stringify(user)
                })

                handleResponse(response, this);

            }
        }
    },
    data(){
        return{
            newpassagain: null,
            newpass:null,
            old:null,
            valid: false,
            Url: process.env.VUE_APP_URL,
            error: false
        }
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/changePass.css);
</style>