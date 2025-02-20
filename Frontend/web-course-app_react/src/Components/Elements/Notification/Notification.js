import React, { useEffect, useState } from "react";
import { signalRService } from '../../../Services/SignalRService';
import "./Notification.css"

const Notification = ({ userEmail }) => {
    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        if (!userEmail) return;

        const handleNotificationsUpdate = (newNotifications) => {
            setNotifications(newNotifications);
        };

        signalRService.addListener(handleNotificationsUpdate);

        setNotifications(signalRService.getNotifications());

        signalRService.startConnection(userEmail);

        return () => {
            signalRService.removeListener(handleNotificationsUpdate);
        };
    }, [userEmail]);

    return (
        <div className="notification-dropdown">
            <h3>Notifications</h3>
            <ul>
                {notifications.map((msg, index) => (
                    <li key={index}>{msg}</li>
                ))}
            </ul>
        </div>
    );
};

export default Notification;