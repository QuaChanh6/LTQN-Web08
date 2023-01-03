<template>
    <div class="container-statistic">
      <div class="title">LƯƠNG NHÂN VIÊN</div>
      <div class="content-statistic-detail">
          <div class="filter">
                <div class="search input-icon">
                    <input type="text" class="input" placeholder="Tìm theo mã, tên nhân viên"  v-model="search" @keyup="searchEmployee">
                    <div class="icon-search" @click="searchEmployee"></div>
                </div>
                <MButton v-if="false" class="btn-save-and-add" :text="'Reset'" @click="reset"/>
                <div class="refresh"></div>
                <div class="import" @click="showSalary =! showSalary"></div>
                <div v-if="showSalary" class="salary-import">
                  <div class="item-hover" @click="downloadExample()">Tải file mẫu</div>
                  <div class="item-hover" @click="importSalary()">Nhập khẩu</div>
                </div>
                <input   :key="keyUpLoad" ref="file" class="fileImport" style="visibility: hidden;" @change="handleFileUpload()"  type="file"/>
                <a :href="href" class="link-export" ref="export" download = 'Import'></a>
          </div>
          <TheTableStatistic @showForm='showFormEdit' :key='keyReloadTable' :search='search' :role="role"/>
      </div>
    </div>
    <TheFormStatistic v-if='isShowFrom' @closeForm='closeForm' :salary='salary' @openToast='openToast' @reload='reload'/>

    <transition name="toast-message">
        <MToastMessage v-show='isShowToast' :content = 'contentOfToastMessage' 
        @closeToastMessage='closeToastMessage' :type='typeToast'/>
    </transition>
    <div class="loading" v-show="isLoading">
        <div class="container"></div>
        <img class="img-load" src="../../../assets/loading.svg" loading="lazy" alt="loading">
    </div>
</template>
  
<script>
import TheTableStatistic from './TheTableStatistic.vue';
import TheFormStatistic from './TheFormStatistic.vue';
import resource from '../../../common/resource';
import MToastMessage from '@/components/base/MToastMessage.vue';
import enumeration from '../../../common/enumeration';
import MButton from '@/components/base/MButton.vue';

async function handleResponse(res, pointer){
        try {
              // thực hiện thành công
              pointer.showSalary = false;
              pointer.reloadAll();
            if(res.ok){
              pointer.openToast(resource.ToastMessageType.success, resource.ToastMessage.success)
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
              console.log(res);
              pointer.openToast(resource.ToastMessageType.error, resource.ToastMessage.error);
            }
    
        } catch (error) {
            console.log(error);
        }
      
    }


  export default {
    components: { TheTableStatistic, TheFormStatistic, MToastMessage, MButton },
    methods:{
       /**
         * Hàm đóng toastmessage
         * author: LTQN(15/9/2022)
         */
         closeToastMessage(){
            this.isShowToast = false;
        },
      downloadExample(){
        let url = `${process.env.VUE_APP_URL}Salaries/file-example`;

            this.isLoading = true;
            fetch(url).then(response => response.blob())
            .then(blob => {
                const reader = new FileReader;
                
                reader.addEventListener('load', () => {
                    this.href = reader.result;
                    this.isLoading = false;
                });
                reader.readAsDataURL(blob);

            }).catch(error => {
                console.log(error);
            })
      },
      reloadAll(){
        this.keyReloadTable = Math.floor(Math.random()*90000);
        this.keyUpLoad = Math.floor(Math.random()*90000);

      },
      async reset(){
        const  response = await fetch(process.env.VUE_APP_URL+'Salaries/salary-reset', {
              method: 'PUT'
            })

            if(response.ok){
              this.reload();
            }
      },
      async handleFileUpload(){
        var data = new FormData();
            data.append('file',  this.$refs.file.files[0]);
            data.append('user', 'admin');

            const  response = await fetch(process.env.VUE_APP_URL+'Salaries/salary-data', {
              method: 'POST',
              body: data
            })
            handleResponse(response, this);
      },
      importSalary(){
          this.$refs.file.click(); 
      },
      showFormEdit(salary){
        let url = process.env.VUE_APP_URL + "Salaries/" + salary.salaryID;
        fetch(url)
            .then(res => res.json())
            .then(res => {
                this.salary = res[0];              
                this.isShowFrom = true;
                
            }).catch(error => {
              console.log(error);
            }) 
        
      },
      closeForm(){
        this.isShowFrom = false;
      },

          /**
         * Hàm mở toastmessage
         * author: LTQN(15/9/2022)
         */
         openToast(type, msg){ 
            try {
                const me=this;
                this.contentOfToastMessage = msg;
                this.isShowToast = true;

                let typeOfToasts = resource.ToastMessageType;
                for(let typeToast in typeOfToasts){
                    if(typeOfToasts[typeToast] === type)
                        this.typeToast = typeOfToasts[typeToast];
                }

                setTimeout(function(){
                    me.isShowToast = false;
                }, 2000);
            } catch (error) {
                console.log(error)
            }
            
        },
  
         /**
         * Hàm reload lại table
         * author: LTQN(10/9/2022)
         */
         reload(){
            this.keyReloadTable = Math.floor(Math.random()*90000);
        },

         /**
         * Hàm tìm kiếm theo họ tên
         * author: LTQN(19/9/2022)
         * @param {*} event sk bàn phím
         */
         searchEmployee(event){
            if(event.keyCode === enumeration.keyCode.ENTER || event.type === resource.event.Click){
                this.reload();
            }
        },

    },
    data() {
      return{
        isShowFrom: false,
        salary: {},
        isShowToast: false,
        contentOfToastMessage: '',
        typeToast: null,
        search: '',
        keyReloadTable: null,
        keyUpLoad: null,
        code: null,
        role: null,
        showSalary: false,
        isLoading: false,
        href: null, //link export
      }
    },
  
    watch:{
        href(){
            setTimeout(() => {{
                this.$refs.export.click(); 
            }},100)

        },
        
      }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/statistic/containerStatistic.css);
</style>
  