<template>
    <div class="container-total">
      <div class="title">TỔNG QUAN NHÂN VIÊN</div>
      <div class="container-overview">
        <MTotal class="employee total" :number="empLength" :text="'Số lượng nhân viên'" :type="typeOverViewEmployee" :icon="'user'"/>
        <MTotal class="position total" :number="depLength" :text="'Số lượng chức vụ'" :type="typeOverViewDepartment" :icon="'building'"/>
        <MTotal class="department total" :number="posLength" :text="'Số lượng phòng ban'" :type="typeOverViewposition" :icon="'briefcase'"/>
      </div>
      <div class="chartTotal">
        <TheChart class="gender" :type="'gender'" :label="labelChart" :color="'#f87979'"></TheChart>
        <TheChart :type="'status'" :label="labelStatus" :color="'#204918'"></TheChart>
      </div>
    </div>
</template>
  
<script>
import MTotal from './MTotal.vue';
import resource from '@/common/resource';
import format from '../../../common/formatData';
import TheChart from '@/components/layout/TheChart.vue';

async function getUserAsync(url) {
        try {
            let response = await fetch(url);
            let data = await response.json();
            return data;
        } catch (error) {   
            console.log(error);
            return [];
        }
        
    }

  export default {
    components: { MTotal, TheChart },
    created(){
      let url = process.env.VUE_APP_URL+'Employees';
            
      getUserAsync(url).then(data =>{
          this.isLoadTable = false;
          this.empLength = data.length;

      });

      url = process.env.VUE_APP_URL+'Departments';

      if(!format.checkEmptyData(localStorage.getItem('department'))){
            this.depLength=JSON.parse(localStorage.getItem('department')).length;
          }
            else{//chưa có dữ liệu trong localstorage
                fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.depLength = res.length;              
                    localStorage.setItem('department', JSON.stringify(res));
                }).catch(error => {
                  console.log(error);
                }) 
            }

          url = process.env.VUE_APP_URL+'Positions';
            if(!format.checkEmptyData(localStorage.getItem('position'))){
            this.posLength=JSON.parse(localStorage.getItem('position')).length;
          }
            else{//chưa có dữ liệu trong localstorage
                fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.posLength = res.length;              
                    localStorage.setItem('position', JSON.stringify(res));
                }).catch(error => {
                  console.log(error);
                }) 
            }


    },
    methods:{
    },
    data(){
      return{
        typeOverViewEmployee: resource.overView.employee,
        typeOverViewDepartment: resource.overView.department,
        typeOverViewposition: resource.overView.position,
        empLength: null,
        depLength: null,
        posLength: null,
        labelChart: ['Nam', 'Nữ', 'Khác'],
        labelStatus: ['Đang thử việc', 'Tạm nghỉ']
      }
    },

}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../../css/total/containerTotal.css);
</style>
  