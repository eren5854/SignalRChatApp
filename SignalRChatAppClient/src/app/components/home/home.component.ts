import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { UserModel } from '../../models/user.model';
import { HttpService } from '../../services/http.service';
import { ChatModel } from '../../models/chat.model';
import { GetChatsModel } from '../../models/getChats.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent{
  userModel: UserModel = new UserModel();
  userId: string | undefined;

  getChatsModel: GetChatsModel = new GetChatsModel();
  chatModel: ChatModel = new ChatModel();

  users: UserModel[] = [];
  chats: ChatModel[] = [];
  selectedUserId: string = "";
  selectedUser: UserModel = new UserModel();
  user = new UserModel();
  hub: signalR.HubConnection | undefined;
  message: string = "";
  lastMessage?: string;
  messageStatus: boolean = false;

  @ViewChild('fileInput') fileInput: ElementRef | undefined;
  @ViewChild('scrollMe') private myScrollContainer: ElementRef | undefined;

  constructor(
    public auth: AuthService,
    private http: HttpService
  ){
    setInterval(()=>{
      this.getUsers();    
    },500)
    this.getCurrentUser();
    this.hubContext();
  }

  hubContext(){
    this.user = this.userModel;

    this.hub = new signalR.HubConnectionBuilder().withUrl("https://localhost:7212/chat-hub").build();
    this.hub.start().then(()=> {
      console.log("Connection is started...");  
      this.messageStatus = false;
      // this.hub?.invoke("Connect", this.user.id);
      this.hub?.on("Users", (res:UserModel) => {
        console.log(res);
        this.users.find(p=> p.id === res.id)!.status = res.status;        
      });

      this.hub?.on("Messages",(res:ChatModel)=> {
        // console.log(res);
        if(this.selectedUserId === res.userId){
          this.chats.push(res);
        }
      });
    });
  }

  getCurrentUser(){
    this.userId = this.auth.user.id;
    this.http.post("User/GetById", {id: this.userId},  (res) => {
      this.userModel = res.data;
    })
  }

  getUsers(){
    this.http.get2<UserModel[]>("Chats/GetAllUsers", (res) => {
      this.users = res.filter((p:UserModel) => p.id != this.userModel.id);
    });
  }

  logout(){
    this.http.post2("User/ChangeUserStatus", {id: this.userModel.id}, (res) => {
      localStorage.clear();
      location.reload();
      console.log(res);
    });
  }

  changeUser(user: UserModel){
    this.selectedUserId = user.id;
    this.selectedUser = user;
    if (user.status === "online") {
      this.messageStatus = true;
    }
    if (user.status === "offline") {
      this.messageStatus = false;
    }

    this.getChatsModel.userId = this.userModel.id;
    this.getChatsModel.toUserId = user.id;

    this.http.post2<ChatModel[]>("Chats/GetChats", this.getChatsModel, (res) => {
      this.chats = res;
      console.log(this.chats[49]);
      // this.lastMessage = this.chats[49].message;
      setTimeout(() => {
        this.scrollToBottom();
      },20);
    });
  }

  sendMessage(){
    if (this.chatModel.message !== "" || this.chatModel.image !== "") {
      const formData = new FormData();
      formData.append("userId", String(this.getChatsModel.userId));
      formData.append("toUserId", String(this.getChatsModel.toUserId));
      formData.append("message", this.message);
      formData.append("image", this.chatModel.image);
      this.lastMessage = this.message;
      this.http.post2<ChatModel>("Chats/SendMessage", formData, (res) => {
        console.log(res);
        this.chats.push(res);
        this.chatModel.image = "";
        this.message = "";
      });
    }
    this.scrollToBottom();
  }

  onKeyUp($event:any){
    if ($event.keyCode === 13) {
      this.sendMessage();
    }
  }

  showEmojiPicker: boolean = false;

  toggleEmojiPicker() {
    this.showEmojiPicker = !this.showEmojiPicker;
  }

  addEmoji(emoji: string) {
    this.message += emoji;
    this.showEmojiPicker = false;
  }
  
  setImage(event: any){
    console.log(event.target.files);
    this.chatModel.image = event.target.files[0];
    this.sendMessage();
  }

  triggerFileInput() {
    this.fileInput!.nativeElement.click();
  }

  scrollToBottom(){
    try {
      this.myScrollContainer!.nativeElement.scrollTop = this.myScrollContainer!.nativeElement.scrollHeight;
    } catch(err) { } 
  }
}