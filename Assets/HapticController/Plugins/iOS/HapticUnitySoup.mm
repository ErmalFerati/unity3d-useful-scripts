#import "HapticUnitySoup.h"

#define SYSTEM_VERSION_GREATER_THAN_10 [[[UIDevice currentDevice] systemVersion] floatValue] > 10.0
#define FeedbackSupport [[[UIDevice currentDevice] valueForKey:@"_feedbackSupportLevel"] intValue]

#pragma mark - HapticUnitySoup

@interface HapticUnitySoup ()
#ifdef SYSTEM_VERSION_GREATER_THAN_10
@property (nonatomic, strong) UINotificationFeedbackGenerator* notificationGenerator;
@property (nonatomic, strong) NSArray<UIImpactFeedbackGenerator*>* impactGenerators;
#endif



@end

@implementation HapticUnitySoup

static HapticUnitySoup * _shared;

+ (HapticUnitySoup*) shared {
    @synchronized(self) {
        if(_shared == nil) {
            _shared = [[self alloc] init];
        }
    }
    return _shared;
}

- (id) init {
    if (self = [super init])
    {
        if(SYSTEM_VERSION_GREATER_THAN_10){
            self.notificationGenerator = [UINotificationFeedbackGenerator new];
            [self.notificationGenerator prepare];
            
            self.impactGenerators = @[
                                      [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleLight],
                                      [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium],
                                      [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleHeavy],
                                      ];
            for(UIImpactFeedbackGenerator* impact in self.impactGenerators) {
                [impact prepare];
            }
        }
    }
    return self;
}

- (void) dealloc {
    if(SYSTEM_VERSION_GREATER_THAN_10){
        self.notificationGenerator = NULL;
        self.impactGenerators = NULL;
    }}

- (bool) notification:(int)type {
    if(SYSTEM_VERSION_GREATER_THAN_10){
        
        if([self isNotificationFeedbackSupport]){
            [self.notificationGenerator notificationOccurred:(UINotificationFeedbackType)type];
            return true;
        }
    }
    
    NSLog(@"iOS not supported false");
    return false;
}

- (bool) impact:(int)style {
    if(SYSTEM_VERSION_GREATER_THAN_10){
        NSLog(@"impact SYSTEM_VERSION_GREATER_THAN_10 %d",FeedbackSupport);
        
        if([self isNotificationFeedbackSupport]){
            NSLog(@"isNotificationFeedbackSupport true");
            [self.impactGenerators[(int) style] impactOccurred];
            return true;
        }
    }
    
    NSLog(@"iOS not supported false");
    return false;
}

- (bool) isNotificationFeedbackSupport {
    //#ifdef SYSTEM_VERSION_GREATER_THAN_10
    //    if ([UINotificationFeedbackGenerator class]) {
    //        return YES;
    //    }
    //#endif
    //    return NO;
    //
    if ((FeedbackSupport == 0) || (FeedbackSupport == 1)){
        return false;
    }else{
        return true;
    }
}

@end

#pragma mark - Unity Bridge

extern "C" {
    bool _unityHapticNotification(int type) {
        return [[HapticUnitySoup shared] notification:(int) type];
    }
    
    bool _unityHapticImpact(int style) {
        return [[HapticUnitySoup shared] impact:(int) style];
    }
}