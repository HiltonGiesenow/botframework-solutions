---
category: Clients and Channels
subcategory: Clients
title:  Virtual Assistant Client (Android)
description: Chat with your Virtual Assistant using the **Virtual Assistant Client**  app and set it up as the default assistant on a device.
order: 1
toc: true
---

# {{ page.title }}
{:.no_toc}
{{page.description}}

## Prerequisites
1. Install [Android Studio](https://developer.android.com/studio/).

1. Download the [**Virtual Assistant Client** app source code](https://aka.ms/virtualassistantclient).

1. [Create a Virtual Assistant]({{site.baseurl}}/virtual-assistant/tutorials/csharp/create-assistant/1_intro/) to setup your Virtual Assistant environment.

1. [Enable speech]({{site.baseurl}}/virtual-assistant/tutorials/enable-speech/1_intro) on your new Virtual Assistant, which enables you to retrieve a
    - [Microsoft Speech Cognitive Service subscription key]({{site.baseurl}}/virtual-assistant/tutorials/enable-speech/2_create_speech_instance/)
    - [Add the Direct Line Speech channel to your Assistant]({{site.baseurl}}/virtual-assistant/tutorials/enable-speech/3_add_speech_channel/)

1. If you want to capture analytics, get started with [Visual Studio App Center](https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/android) and register a new app.

## Build and run

### Add your application settings
{:.no_toc}

There are two configuration files used to provide your environment settings.

#### [Direct Line Speech configuration]({{site.repo}}/blob/next/samples/android/clients/VirtualAssistantClient/directlinespeech/src/main/assets/default_configuration.json)
{:.no_toc}
```json
{
  "service_key": "SPEECH_SERVICE_SUBSCRIPTION_KEY", // Replace with your Speech Service subscription key
  "service_region": "westus2",
  "bot_id": "DIRECT_LINE_SPEECH_SECRET_KEY", // Replace with your Direct Line Speech secret
  "user_id": "android",
  "user_name": "Android",
  "locale": "en-us",
  "keyword": "computer"
}
```

The **user_id** is a unique identifier for all messages generated by the user, this can be combined with [Linked Accounts sample]({{site.baseurl}}/virtual-assistant/samples/linked-accounts/).

#### [App configuration]({{site.repo}}/blob/next/samples/android/clients/VirtualAssistantClient/app/src/main/assets/default_app_configuration.json)
{:.no_toc}
```json
{
  "history_linecount": 2147483646,
  "show_full_conversation": true,
  "enable_dark_mode": false,
  "keep_screen_on": true,
  "app_center_id": "APP_CENTER_ID" // Replace with your Visual Studio App Center id
}
```

#### Optional: [Chat colors]({{site.repo}}/blob/next/samples/android/clients/VirtualAssistantClient/app/src/main/res/values/colors.xml)
{:.no_toc}
```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
...
    <!-- Chat -->
    <color name="color_chat_text_bot">#000000</color>
    <color name="color_chat_text_user">#ffffff</color>
    <color name="color_chat_background_bot">#f2f2f2</color>
    <color name="color_chat_background_user">#3062d6</color>
...
</resources>
```

### Run
{:.no_toc}
[Build and run your app](https://developer.android.com/studio/run) to deploy to the Android Emulator or a connected device.

#### Permissions
{:.no_toc}
##### Record Audio
{:.no_toc}
Required for the user to make voice requests to a bot. With this a user can only use the keyboard.
##### Fine Location
{:.no_toc}
Allow Virtual Assistant to receive the [**VA.Location** event]({{site.baseurl}}/virtual-assistant/handbook/events/) with GPS coordinates to utilize location-based skills like Point of Interest.

## Interact with a Virtual Assistant
### Chat
{:.no_toc}
The main view shows an expected user and assistant chat window. Start a conversation by selecting the microphone or keyboard icons.

![Widgets]({{site.baseurl}}/assets/images/android-virtual-assistant-client-chat.png)

### Widget
{:.no_toc}
Using widgets, you can demonstrate an Assistant having a native chat experience on a device.

![Widgets]({{site.baseurl}}/assets/images/android-virtual-assistant-client-widget.png)

### Side menu
{:.no_toc}
Swipe from the left to access the menu.

![Side menu]({{site.baseurl}}/assets/images/android-virtual-assistant-client-side-menu.png)

### Restart conversation
{:.no_toc}
Restart the conversation with a Virtual Assistant with a new conversation id.

### Settings
{:.no_toc}
Access the same settings from the configuration files.

![Settings]({{site.baseurl}}/assets/images/android-virtual-assistant-client-settings.png)

### Set as default assistant
{:.no_toc}

Set your Virtual Assistant as the device's default assist app.

1. Allow the **Appear on top** permission to overlay this app on Android
![Settings]({{site.baseurl}}/assets/images/android-virtual-assistant-client-appear-on-top.png)

1. Select **Device assistance app**
![Settings]({{site.baseurl}}/assets/images/android-virtual-assistant-client-device-assistance-app-1.png)

1. Select **Virtual Assistant**
![Settings]({{site.baseurl}}/assets/images/android-virtual-assistant-client-device-assistance-app-2.png)

## Events
The **Virtual Assistant Client** is enabled to work with [events used in the sample Skills]({{site.baseurl}}/virtual-assistant/handbook/events/). 

### Open default apps
{:.no_toc}
#### [OpenDefaultApp](https://github.com/microsoft/botframework-solutions/blob/8e05d16bacaac483810807cab67b9120d07c5302/samples/android/clients/VirtualAssistantClient/app/src/main/java/com/microsoft/bot/builder/solutions/virtualassistant/service/SpeechService.java#L502)
{:.no_toc}
This method takes the metadata from an **OpenDefaultApp** event to open default apps on the device.

#### Maps
{:.no_toc}
Compatible with either [Waze](https://www.waze.com/) or [Google Maps](https://www.google.com/maps) (in this order).

#### Phone
{:.no_toc}
Compatible with the default dialer.

#### Music
{:.no_toc}
Compatible with [Spotify](https://www.spotify.com/).

### Other events
{:.no_toc}
#### [BroadcastWidgetUpdate](https://github.com/microsoft/botframework-solutions/blob/next/samples/android/clients/VirtualAssistantClient/app/src/main/java/com/microsoft/bot/builder/solutions/virtualassistant/service/SpeechService.java#L579)
{:.no_toc}
This method sends the value of this event activity to any listening apps, like the [**Event Companion**]({{site.baseurl}}/virtual-assistant/samples/event-companion/) app.

## Next steps
Use the [**Event Companion**]({{site.baseurl}}/virtual-assistant/samples/event-companion/) app to broadcast your Virtual Assistant's metadata and prototype advanced scenarios.