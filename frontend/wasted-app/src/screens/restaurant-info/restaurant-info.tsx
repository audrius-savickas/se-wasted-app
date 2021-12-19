import React, {useState} from "react"
import {Alert, Linking, StyleSheet} from "react-native"
import {ScrollView} from "react-native-gesture-handler"
import {Colors, ExpandableSection, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {Map} from "../../components/map"
import {navigateToFoodList} from "../../services/navigation"
import {callNumber} from "../../utils/call"
import {formatDistance} from "../../utils/coordinates"
import {RestaurantInfoProps} from "./interfaces"

export const RestaurantInfo = ({componentId, restaurant}: RestaurantInfoProps) => {
  const {name, address, foodCount, description, distanceToUser, coords} = restaurant
  const [descriptionExpanded, setDescriptionExpanded] = useState(true)

  const emailRestaurant = async () => {
    const url = `mailto:${restaurant.mail}`
    if (await Linking.canOpenURL(url)) {
      await Linking.openURL(`mailto:${restaurant.mail}`)
    } else {
      Alert.alert("Email is not available.")
    }
  }

  return (
    <ScrollView>
      <View margin-s4>
        <View centerH>
          <Text text30M purple20 marginT-s2 marginB-s1>
            {name}
          </Text>
          <View marginT-s2 style={styles.shadow}>
            <Image source={{uri: restaurant.imageURL, height: 220, width: 350}} style={{height: 220, width: 350}} />
          </View>
        </View>
        <View marginT-s6 marginH-s6>
          <View row centerV>
            <Text text60L purple20 style={{width: 120}}>
              Location
            </Text>
            <Text text60L flex>
              {address}
            </Text>
          </View>
          <View row centerV marginT-s4>
            <Text text60L purple20 style={{width: 120}}>
              Distance
            </Text>
            <Text text60L>{formatDistance(distanceToUser)} km</Text>
          </View>
        </View>
        <View centerV marginT-s4 marginH-s6>
          <ExpandableSection
            sectionHeader={
              <View row>
                <Text text60L purple20 marginR-s2>
                  Description
                </Text>
                <Image
                  style={{
                    alignSelf: "center",
                    width: 16,
                    height: 16,
                    transform: [{scaleY: descriptionExpanded ? -1 : 1}]
                  }}
                  source={require("../../../assets/down-chevron.png")}
                />
              </View>
            }
            expanded={descriptionExpanded}
            onPress={() => setDescriptionExpanded(!descriptionExpanded)}
          >
            <Text marginT-s1 text80L>
              {description}
            </Text>
          </ExpandableSection>
        </View>
        <View marginH-s4 marginT-s4>
          <Map
            coordinates={coords}
            coordinatesDelta={{latitudeDelta: 0.003, longitudeDelta: 0.003}}
            style={styles.map}
          />
        </View>
        <View marginT-s6 marginH-s4>
          <Text text60L purple20>
            Contact details
          </Text>
          <TouchableOpacity
            bg-white
            row
            centerV
            marginT-s2
            paddingV-s2
            paddingH-s6
            style={styles.contact}
            onPress={emailRestaurant}
          >
            <Image source={require("../../../assets/mail-30x30.png")} />
            <Text marginL-s4 text70R>{`Email: `}</Text>
            <Text text70L>{`${restaurant.mail}`}</Text>
          </TouchableOpacity>
          <TouchableOpacity
            bg-white
            row
            centerV
            marginT-s2
            paddingV-s2
            paddingH-s6
            style={styles.contact}
            onPress={() => callNumber(restaurant.phone)}
          >
            <Image source={require("../../../assets/phone-30x30.png")} />
            <Text marginL-s4 text70R>{`Phone number: `}</Text>
            <Text text70L>{`${restaurant.phone}`}</Text>
          </TouchableOpacity>
        </View>

        <View marginT-s8 center>
          <TouchableOpacity
            bg-purple30
            br60
            paddingH-s4
            paddingV-s2
            style={styles.shadowButton}
            onPress={() => navigateToFoodList(componentId, {idRestaurant: restaurant.id})}
          >
            <Text text60L white center>
              See restaurant's list of food ↗️{"\n"}({foodCount} {foodCount === 1 ? "item" : "items"})
            </Text>
          </TouchableOpacity>
        </View>
      </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  shadow: {shadowColor: Colors.black, shadowOpacity: 0.4, shadowOffset: {height: 0, width: 0}},
  shadowButton: {shadowColor: Colors.black, shadowOpacity: 0.7, shadowOffset: {height: 0, width: 0}},
  map: {
    height: 200
  },
  contact: {
    borderColor: Colors.grey40,
    borderWidth: 1,
    borderRadius: 20,
    shadowColor: Colors.black,
    shadowRadius: 2,
    shadowOffset: {width: 0, height: 0},
    shadowOpacity: 0.3
  },
  button: {
    width: 150,
    alignSelf: "center"
  },
  error: {position: "absolute", alignSelf: "center", width: "85%"}
})
