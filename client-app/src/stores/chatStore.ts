import { action, makeAutoObservable, observable } from "mobx"

class ChatStore {
  sendeeName: string | null = null
  sendeePhoto: string | undefined
  isShown: boolean = false
  
  constructor() {
    makeAutoObservable(this, {
      sendeeName: observable,
      sendeePhoto: observable,
      isShown: observable,
      showChat: action,
      hideChat : action
    })
  }

  showChat(name: string, photo: string) {
    this.isShown = true;
    this.sendeeName = name;
    this.sendeePhoto = photo;
  }

  hideChat() {
    this.isShown = false;
    this.sendeeName = null;
    this.sendeePhoto = undefined;
  }
}

const chatStore = new ChatStore();

export default chatStore;