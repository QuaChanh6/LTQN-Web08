<template>
     <div class="pop-up-container">
        <div class="container"></div>
        <div class="pop-up  version1">
            <div class="pop-up-content">
                <div class="icon">
                    <div class="icon-popUp"></div>
                </div>
                <div class="pop-up-text">Bạn có thực sự muốn xóa nhân viên  <span>000579</span> không?</div>
            </div>
            <div class="pop-up-action">
                <!-- <button class="button button-pop-up-no">Không</button>
                <button class="button button-pop-up-yes">Có</button> -->
                <MButton class="button-pop-up-no" :text= 'button.No'/>
                <MButton class="button-pop-up-yes" :text= 'button.Yes'/>
            </div>
        </div>
            
    </div>
</template>
  
<script>
  export default {
    data(){
        return{
            button: {
              No: 'Không',
              Yes: 'Có'
            }
        }
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/popup.css);
</style>
  