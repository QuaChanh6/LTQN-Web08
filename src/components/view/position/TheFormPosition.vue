<template>
    <div class="container-form-position">
        <div class="container"></div>
        <div class="form-position">
            <div class="form-header">
                <div class="title-position">Thông tin chức vụ</div>
                <div class="icon-x" @click="closeForm"></div>
            </div>

            <div class="form-body">
                <div class="row code-position">
                    <label>Mã chức vụ:</label>
                    <input type="text" class="input" placeholder="Mã phòng ban" v-model="pos.PositionCode">
                </div>
                <div class="row name-position">
                    <label>Tên chức vụ:</label>
                    <input type="text" class="input" placeholder="Tên phòng ban" v-model="pos.PositionName">

                </div>
                <div class="row name-managar">
                    <label>Lương khởi điểm:</label>
                    <input type="text" class="input" placeholder="Lương khởi điểm" v-model="pos.Salary">

                </div>
            </div>

            <div class="form-footer">
                <div class="cancel">
                    <MButton :text="'Hủy'" @click="closeForm"/>
                </div>
                <div class="save">
                    <MButton :text="'Cất'" @click="addPos"/>
                   
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
        async addPos(){
            let response = await fetch(`${this.Url}Positions`,{
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            method: "POST",
            body: JSON.stringify(this.pos)
            })
            this.$emit('closeForm');
            console.log(response);
            localStorage.removeItem('position');
            this.$emit('reload');
        }
    },
    data(){
        return{
            pos: {},
            Url: process.env.VUE_APP_URL,

        }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/position/formPosition.css);
  </style>
  