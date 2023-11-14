import { action, makeAutoObservable, observable } from "mobx";

class ChatProfileStore {
    isItShown: boolean = true;

    constructor() {
        makeAutoObservable(this, {
            isItShown: observable,
            changeIsItShownState: action
        })
    }

    changeIsItShownState() {
        this.isItShown = !this.isItShown;
    }
}

const chatProfileStore = new ChatProfileStore();
export default chatProfileStore;