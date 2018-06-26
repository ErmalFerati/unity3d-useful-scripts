#ifndef HapticUnitySoupPlugin_h
#define HapticUnitySoupPlugin_h

#import <UIKit/UIKit.h>


@interface HapticUnitySoup : NSObject{
    
}
+ (HapticUnitySoup*) shared;
- (bool) notification:(int) type;
- (bool) impact:(int) style;
@end

#endif