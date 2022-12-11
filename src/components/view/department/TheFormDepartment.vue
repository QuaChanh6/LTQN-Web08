<template>
    <div class="container-form-department">
        <div class="container"></div>
        <div class="form-department">
            <div class="form-header">
                <div class="title-department">Thông tin phòng ban</div>
                <div class="icon-x" @click="closeForm"></div>
            </div>

            <div class="form-body">
                <div class="row code-department">
                    <label>Mã phòng ban:</label>
                    <input type="text" class="input" placeholder="Mã phòng ban" v-model="dep.DepartmentCode">
                </div>
                <div class="row name-department">
                    <label>Tên phòng ban:</label>
                    <input type="text" class="input" placeholder="Tên phòng ban" v-model="dep.DepartmentName">

                </div>
                <div class="row name-managar">
                    <label>Tên quản lý:</label>
                    <input type="text" class="input" placeholder="Tên quản lý" v-model="dep.Manager">

                </div>
            </div>

            <div class="form-footer">
                <div class="cancel">
                    <MButton :text="'Hủy'" @click="closeForm"/>
                </div>
                <div class="save">
                    <MButton :text="'Cất'" @click="addDepartment"/>
                   
                </div>
        </div>
        </div>
    </div>
  </template>
  
  <script>
import MButton from '@/components/base/MButton.vue';
  export default {
    components: { MButton },
    methods:{
        closeForm(){
            this.$emit('closeForm');
        },

        async addDepartment(){
            let response = await fetch(`${this.Url}Departments`,{
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            method: "POST",
            body: JSON.stringify(this.dep)
            })
            this.$emit('closeForm');
            console.log(response);
            localStorage.removeItem('department');
            this.$emit('reload');
        }
    },
    data(){
        return{
            dep: {},
            Url: process.env.VUE_APP_URL,
        }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/department/formDepartment.css);
  </style>
  