<template>
    <div class="container-statistic">
      <div class="title">LƯƠNG NHÂN VIÊN</div>
      <div class="content-statistic-detail">
          <div class="filter">
                <div class="search input-icon">
                    <input type="text" class="input" placeholder="Tìm theo mã, tên nhân viên"  v-model="search" @keyup="searchEmployee">
                    <div class="icon-search" @click="searchEmployee"></div>
                </div>
                <div class="refresh"></div>
                <div class="import" @click="importSalary()"></div>
                <input   :key="keyUpLoad" ref="file" class="fileImport" style="visibility: hidden;" @change="handleFileUpload()"  type="file"/>
          </div>
          <TheTableStatistic @showForm='showFormEdit' :key='keyReloadTable' :search='search'/>
      </div>
    </div>
    <TheFormStatistic v-if='isShowFrom' @closeForm='closeForm' :salary='salary' @openToast='openToast' @reload='reload'/>

    <transition name="toast-message">
        <MToastMessage v-show='isShowToast' :content = 'contentOfToastMessage' 
        @closeToastMessage='closeToastMessage' :type='typeToast'/>
    </transition>

</template>
  
<script>
import TheTableStatistic from './TheTableStatistic.vue';
import TheFormStatistic from './TheFormStatistic.vue';
import resource from '../../../common/resource';
import MToastMessage from '@/components/base/MToastMessage.vue';
import enumeration from '../../../common/enumeration';


async function handleResponse(res, pointer){
        try {
              // thực hiện thành công
            if(res.ok){
              console.log(pointer);
              pointer.reloadAll();
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
              console.log(res);
              alert("File không đúng định dạng!")
            }
    
        } catch (error) {
            console.log(error);
        }
      
    }


  export default {
    components: { TheTableStatistic, TheFormStatistic, MToastMessage },
    methods:{
      reloadAll(){
        this.keyReloadTable = Math.floor(Math.random()*90000);
        this.keyUpLoad = Math.floor(Math.random()*90000);

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
        closeToastMessage(){
          this.isShowToast = false;
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
        keyUpLoad: null
      }
    },
  
    
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/statistic/containerStatistic.css);
</style>
  