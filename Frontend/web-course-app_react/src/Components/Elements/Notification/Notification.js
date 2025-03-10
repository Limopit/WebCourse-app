import React, { useEffect, useState } from "react";
import { signalRService } from '../../../Services/SignalRService';
import "./Notification.css";

const Notification = ({ userEmail }) => {
    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        if (!userEmail) return;

        const handleNotificationsUpdate = (newNotifications) => {
            setNotifications((prevNotifications) => [...newNotifications, ...prevNotifications]);
        };

        signalRService.addListener(handleNotificationsUpdate);

        const loadNotifications = async () => {
            const notificationsFromServer = await signalRService.getNotifications(userEmail);
            setNotifications(notificationsFromServer);
        };

        loadNotifications();

        signalRService.startConnection(userEmail);

        return () => {
            signalRService.removeListener(handleNotificationsUpdate);
        };
    }, [userEmail]);

    const formatTimestamp = (timestamp) => {
        const date = new Date(timestamp);
        return date.toLocaleString();
    };

    return (
        <div className="notification-dropdown">
            <h3>Notifications</h3>
            <ul>
                {notifications.map((notification) => (
                    <li key={notification.notificationId}>
                        <div className="notification-message">{notification.message}</div>
                        <div className="notification-timestamp">
                            {formatTimestamp(notification.timestamp)}
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default Notification;