import { HubConnectionBuilder } from '@microsoft/signalr';

class SignalRService {
    constructor() {
        if (SignalRService.instance) {
            return SignalRService.instance;
        }

        this.connection = null;
        this.notifications = [];
        this.listeners = [];
        SignalRService.instance = this;
    }

    startConnection = async (userEmail) => {
        if (!userEmail || this.connection) return;

        this.connection = new HubConnectionBuilder()
            .withUrl("https://localhost:5004/notificationHub", {
                accessTokenFactory: () => sessionStorage.getItem("accessToken"),
            })
            .withAutomaticReconnect()
            .build();

        try {
            await this.connection.start();
            console.log('SignalR Connected!');

            this.connection.on('ReceiveNotification', (message) => {
                console.log('Received Notification:', message);
                this.notifications = [...this.notifications, message];
                this.notifyListeners();
            });

            this.notifyListeners();
        } catch (err) {
            console.error('SignalR Connection Error: ', err);
        }
    };

    stopConnection = () => {
        if (this.connection) {
            this.connection.stop();
            this.connection = null;
        }
    };

    addListener = (listener) => {
        this.listeners = [...this.listeners, listener];
    };

    removeListener = (listener) => {
        this.listeners = this.listeners.filter(l => l !== listener);
    };

    notifyListeners = () => {
        this.listeners.forEach(listener => listener(this.notifications));
    };

    getNotifications = () => {
        return this.notifications;
    };
}

export const signalRService = new SignalRService();