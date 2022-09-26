<template>
     <div class="pop-up-container">
        <div class="container"></div>
        <div class="pop-up">
            <div class="pop-up-content">
                <div class="icon">
                    <div class="icon-popUp"></div>
                </div>
                
                <div class="pop-up-text">{{text}}</div>
            </div>
            <div class="pop-up-action">
                <MButton class="button-pop-up-no" :text= 'button.No' @click="sendMessageClose()"/>
                <MButton class="button-pop-up-yes" :text= 'button.Yes' @click="sendMessageDelete()"/>
            </div>
        </div>
            
    </div> 
</template>

<script>
import MButton from './MButton.vue';
  export default {
    props: {
      deleteEmployee: Object,
      content: String
    },
    data() {
        return {
            button: {
                No: "Không",
                Yes: "Có"
            },
            text:`Bạn có thực sự muốn xóa nhân viên <${this.deleteEmployee.employeeCode}> không?`,

        };
    },
    methods: {
      /**
       * gửi lên cha để đóng pop up
       * author: LTQN(10/9/2022)
       */
      sendMessageClose(){
        this.$emit('closePopup');
      },
      /**
       * gửi lên cha để xóa nhân viên
       * author: LTQN(10/9/2022)
       */
      sendMessageDelete(){
        this.$emit('deleteEmp', this.deleteEmployee.employeeID);
      }
    },
    components: { MButton }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/popup.css);
</style>
  