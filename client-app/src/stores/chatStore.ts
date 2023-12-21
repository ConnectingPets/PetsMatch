import { action, makeAutoObservable, observable } from "mobx"

class ChatStore {
  sendeeName: string | null = null
  sendeePhoto: string | undefined
  isShown: boolean = false
  matchId: string | null = null
  
  constructor() {
    makeAutoObservable(this, {
      sendeeName: observable,
      sendeePhoto: observable,
      isShown: observable,
      matchId: observable,
      showChat: action,
      hideChat : action
    })
  }

  showChat(name: string, photo: string, matchId: string) {
    this.isShown = true;
    this.sendeeName = name;
    this.sendeePhoto = photo;
    this.matchId = matchId;
  }

  hideChat() {
    this.isShown = false;
    this.sendeeName = null;
    this.sendeePhoto = undefined;
    this.matchId = null;
  }
}

const chatStore = new ChatStore();

export default chatStore;