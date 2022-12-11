<template>
     <div class="pop-up-container">
        <div class="container"></div>
        <div class="pop-up" :class="type"  ref="focus" tabindex="-1">
            <div class="pop-up-content">
                <div class="icon">
                    <div class="icon-popUp"></div>
                </div>
                
                <div class="pop-up-text">{{textPopUp}}</div>
            </div>
            <div class="pop-up-action two-button">
                  <MButton class="button-pop-up-no" :text= 'button.No' @click="sendMessageClose()"/>
                  <MButton class="button-pop-up-yes" :text= 'button.Yes' @click="sendMessageDelete()"/>
            </div>
            <div class="pop-up-action three-button">
                <MButton class="button-pop-up-cancel" :text= 'button.Cancel' @click="sendMessageClose()"/>
                <div>
                  <MButton class="button-pop-up-no button-no" :text= 'button.No' @click="sendMessageCloseForm()"/>
                  <MButton class="button-pop-up-yes" :text= 'button.Yes' @click="sendMessageSave()"/>
                </div>
            </div>
        </div>
            
    </div> 
</template>

<script>
import MButton from './MButton.vue';
  export default {
    props: {
      deleteEmployee: Object,
      content: String,
      type: String,
      textPopUp: String
    },
    data() {
        return {
            button: {
                No: "Không",
                Yes: "Có",
                Cancel: "Hủy"
            },
        };
    },
    mounted(){
        this.$refs.focus.focus();
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
        if(this.deleteEmployee.employeeID != null){
          this.$emit('deleteEmp', this.deleteEmployee.employeeID);
        }else{
          this.$emit('deleteEmpMultiple');
        }
      },

      /**
       * Gửi msg để đóng form
       */
      sendMessageCloseForm(){
        this.$emit('closePopup');
        this.$emit('closeFrom');
      },

      /**
       * Gửi msg để cất dữ liệu
       */
      sendMessageSave(){
        this.$emit('closePopup');
        this.$emit('saveForm');
      },

    },

    components: { MButton },

}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/popup.css);
</style>
  