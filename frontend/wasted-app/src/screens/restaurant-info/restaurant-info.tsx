import React, {useState} from "react"
import {StyleSheet} from "react-native"
import {ScrollView} from "react-native-gesture-handler"
import {Colors, ExpandableSection, Image, Text, TouchableOpacity, View} from "react-native-ui-lib"
import {navigateToFoodList} from "../../services/navigation"
import {formatDistance} from "../../utils/coordinates"
import {RestaurantInfoProps} from "./interfaces"

export const RestaurantInfo = ({componentId, restaurant}: RestaurantInfoProps) => {
  const {name, address, foodCount, description, distanceToUser} = restaurant
  const [descriptionExpanded, setDescriptionExpanded] = useState(true)

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
            <Text>{formatDistance(distanceToUser)} km</Text>
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
        <View marginT-s6 center>
          <TouchableOpacity
            bg-purple30
            br60
            paddingH-s4
            paddingV-s2
            onPress={() =>
              navigateToFoodList(componentId, {restaurantId: restaurant.id, restaurantName: restaurant.name})
            }
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
  shadow: {shadowColor: Colors.black, shadowOpacity: 0.4, shadowOffset: {height: 0, width: 0}}
})
